using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DataCollection;
using System.Data.Entity;
using Helper;
using Newtonsoft.Json;

namespace Repository
{
    public abstract class BaseRepository<TEntity, TSing> where TEntity : class
    {
        private static readonly Lazy<TSing> _instance = new Lazy<TSing>(() =>
        {
            if (!typeof(TSing).IsSealed)
                throw new Exception(String.Format("仓储{0}必须声明为sealed！", typeof(TSing)));
            var ctors = typeof(TSing).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (ctors.Count() != 1)
                throw new Exception(String.Format("类型{0}必须有构造函数！", typeof(TSing)));
            var ctor = ctors.SingleOrDefault(c => c.GetParameters().Count() == 0 && c.IsPrivate);
            if (ctor == null)
                throw new Exception(String.Format("{0}必须有私有且无参的构造函数", typeof(TSing)));
            return (TSing)ctor.Invoke(null);
        });
        public static TSing Instance
        {
            get { return _instance.Value; }
        }

        private static readonly string _dbContextKey = "_dbContextDbContextKey";
        private static DataCollection.Collection _dbContext
        {
            get
            {
                Collection collection = CallContext.GetData(_dbContextKey) as Collection;
                if (collection == null)
                {
                    collection = new Collection(ConfigurationManager.AppSettings["DbConnection"]);
                    CallContext.SetData(_dbContextKey, collection);
                }
                return collection;
            }
        }
     

        public virtual IQueryable<TEntity> Source
        {
            get { return _dbContext.Set<TEntity>(); }
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.Set<TEntity>().Where(expression);
        }

        public virtual DataTable ExecuteSqlToTable(string sql, params object[] Obj)
        {
            return (DataTable)_dbContext.Database.SqlQuery<TEntity>(sql, Obj).AsQueryable();
        }

        public virtual List<T> ExecuteSql<T>(string sql, params object[] obj) where T : class
        {
            return _dbContext.Database.SqlQuery<T>(sql, obj).ToList();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.Set<TEntity>().Count(expression);
        }

        public virtual IQueryable<TEntity> FindForPaging(int size, int index, Expression<Func<TEntity, bool>> expression, out int total)
        {
            return FindForPaging(size, index, this.Find(expression), out total);
        }

        public virtual IQueryable<TEntity> FindForPaging(int size, int index, IQueryable<TEntity> source, out int total)
        {
            if (index <= 0)
                index = 1;
            var temp = source.Skip((index - 1) * size).Take(size);
            total = source.Count();
            return temp;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Insert(TEntity entity)
        {
            bool result = false;
            if (entity == null)
            {
                throw new Exception("新增对象不能为空");
            }

            CatchEfException(() =>
            {
                _dbContext.Set<TEntity>().Add(entity);
                result = _dbContext.SaveChanges() > 0;
            });

            return result;
        }

        /// <summary>
        /// 添加并返回最新对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity InsertReturnEntity(TEntity entity)
        {
            CatchEfException(() =>
            {
                if (entity == null)
                    throw new Exception("新增对象不能为空");
                _dbContext.Set<TEntity>().Add(entity);
                _dbContext.SaveChanges();
            });

            return entity;
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="batch"></param>
        /// <returns></returns>
        public virtual bool Insert(IEnumerable<TEntity> batch)
        {
            bool result = false;
            CatchEfException(() =>
            {
                _dbContext.Set<TEntity>().AddRange(batch);
                result = _dbContext.SaveChanges() > 0;
            });

            return result;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Save(TEntity entity)
        {
            bool result = false;
            if (entity == null)
            {
                throw new Exception("保存对象不能为空");
            }

            CatchEfException(() =>
            {
                _dbContext.Entry<TEntity>(entity).State = EntityState.Modified;
                result = _dbContext.SaveChanges() > 0;
            });

            return result;
        }

        public virtual bool Delete(TEntity entity)
        {
            if (entity == null)
                throw new Exception("删除对象不能为空");
            _dbContext.Set<TEntity>().Remove(entity);
            return _dbContext.SaveChanges() > 0;
        }

        public virtual bool Delete(IEnumerable<TEntity> batch)
        {
            _dbContext.Set<TEntity>().RemoveRange(batch);
            return _dbContext.SaveChanges() > 0;
        }

        public virtual bool Delete(Expression<Func<TEntity, bool>> expression)
        {
            return this.Delete(this.Find(expression));
        }

        /// <summary>
        /// 开启事务-可以在事务期间读取可变数据，但是不可以修改，也不可以添加任何新数据
        /// </summary>
        /// <param name="action"></param>
        public void Transaction(Action action)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    action();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    scope.Dispose();
                }
            }
        }

        /// <summary>
        /// 开启事务-不可以在事务期间读取可变数据，但是可以修改它
        /// </summary>
        /// <param name="action"></param>
        public void Transaction_dbContextCommit(Action action)
        {
            TransactionOptions transOptions = new TransactionOptions();
            transOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, transOptions))
            {
                try
                {
                    action();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    scope.Dispose();
                }
            }
        }

        /// <summary>
        /// 捕获EF异常
        /// </summary>
        /// <param name="action"></param>
        private void CatchEfException(Action action)
        {
            try
            {
                action();
            }
            catch (DbEntityValidationException ex)
            {
                //当抛出异常时，将EF本地实体清除，防止同一线程插入或者更新失败，下次更新数据库时将原有实体同时提交，引起异常
                _dbContext.Set<TEntity>().Local.Clear();
                string errorMsg = string.Empty;
                foreach (var efError in ex.EntityValidationErrors)
                {
                    foreach (var validError in efError.ValidationErrors)
                    {
                        errorMsg += string.Format("字段名：{0},错误信息：{1}\n\r", validError.PropertyName,
                            validError.ErrorMessage);
                    }
                }
                LogHelper.Error(errorMsg, ex);
                throw;
            }
            catch (DbUpdateException ex)
            {
                //当抛出异常时，将EF本地实体清除，防止同一线程插入或者更新失败，下次更新数据库时将原有实体同时提交，引起异常
                _dbContext.Set<TEntity>().Local.Clear();
                string errorMsg = string.Empty;
                foreach (var e in ex.Entries)
                {
                    errorMsg += string.Format("实体：{0}", JsonConvert.SerializeObject(e.Entity));
                }
                errorMsg += string.Format("错误信息:{0}", ex.InnerException.InnerException.Message);
                LogHelper.Error(errorMsg, ex);
                throw;
            }
        }
    }
}
