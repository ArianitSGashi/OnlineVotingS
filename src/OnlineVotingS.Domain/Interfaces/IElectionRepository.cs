﻿using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Domain.Interfaces;

public interface IElectionRepository : IGenericRepository<Elections>
{
    Task<Elections> GetByTitleAsync(string title);
    Task<IEnumerable<Elections>> GetActiveElectionsAsync();

    Task<IEnumerable<Elections>> GetElectionsByTitleAsync(string title);

    Task<IEnumerable<Elections>> GetUpcomingElectionsAsync(DateTime date);
}
