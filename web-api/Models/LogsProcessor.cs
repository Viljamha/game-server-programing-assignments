using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using web_api.Repositories;

namespace web_api.Models
{
    public class LogsProcessor
    {
        private IRepository repository;

        public LogsProcessor(IRepository rep) {
            repository = rep;
        }

        public Task<Log[]> GetAllLogs() {
            return repository.GetAllLogs();
        }
    }
}