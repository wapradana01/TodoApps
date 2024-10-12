using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Api.DataAccess;
using Todo.Api.DataAccess.Models;
using Todo.Api.Shared.Objects;

namespace Todo.Api.Core.Services.Master
{
    public class DocumentNumberService(ApplicationDbContext dbContext, IMapper mapper, ILogger<DocumentNumberService> logger)
        : BaseService<DocumentNumberService>(dbContext, mapper, logger)
    {
        public async Task<DocNumberConfig> GetRunningNumberDocument(string transactionTypeCode)
        {
            var docNumberConfig = await _dbContext.DocNumberConfigs.FirstOrDefaultAsync(x => x.TransactionTypeCode == transactionTypeCode);

            if (docNumberConfig == null)
            {
                docNumberConfig = new DocNumberConfig
                {
                    TransactionTypeCode = transactionTypeCode,
                    RunningNumber = 0
                };

                await _dbContext.DocNumberConfigs.AddAsync(docNumberConfig);
                await _dbContext.SaveChangesAsync();
            }

            await UpdateRunningNumber(docNumberConfig.Id);

            return docNumberConfig;
        }

        private async Task UpdateRunningNumber(Guid idData)
        {
            var docNumberConfig = await _dbContext.DocNumberConfigs.FirstOrDefaultAsync(x => x.Id == idData) ??
                throw new DbUpdateException($"Gagal melakukan update running number dokumen");

            docNumberConfig.RunningNumber++;

            _dbContext.DocNumberConfigs.Update(docNumberConfig);
            await _dbContext.SaveChangesAsync();
        }
    }
}
