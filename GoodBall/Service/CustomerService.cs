using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollection.Entity;
using Repository;
using Service.Cond;
using Service.Dto;

namespace Service
{
    public class CustomerService : SingModel<CustomerService>
    {
        private static readonly CustomerRepository CustomerRepository = CustomerRepository.Instance;

        private CustomerService() { }

        public List<CustomerDto> GetCustomerListByPage(string question, int size, int index, out int total)
        {
            var query = CustomerRepository.Source;
            if (!string.IsNullOrEmpty(question))
            {
                query = query.Where(x => x.Question.Contains(question));
            }
            query = query.OrderByDescending(x => x.CreateTime);
            return CustomerRepository.FindForPaging(size, index, query, out total).ToList().ToListModel<Customer, CustomerDto>();
        }

        public CustomerDto GetCustomer(long id)
        {
            return CustomerRepository.Find(x => x.Id == id).FirstOrDefault().ToModel<CustomerDto>();
        }

        public void AddCustomer(CustomerDto dto)
        {
            var entity = dto.ToModel<Customer>();
            CustomerRepository.Insert(entity);
        }

        public void UpdateCustomer(CustomerDto dto)
        {
            CustomerRepository.Save(x => x.Id == dto.Id, x => new Customer { Answer = dto.Answer, AnswerTime = DateTime.Now });
        }

      
        public void DeleteCustomer(long id)
        {
            CustomerRepository.Delete(x => x.Id == id);
        }
    }
}
