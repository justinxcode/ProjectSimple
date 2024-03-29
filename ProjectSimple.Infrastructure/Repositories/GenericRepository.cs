﻿using Microsoft.EntityFrameworkCore;
using ProjectSimple.Application.Interfaces;
using ProjectSimple.Application.Models;
using ProjectSimple.Domain.Models.Common;
using ProjectSimple.Infrastructure.DatabaseContext;
using System.Linq.Expressions;
using static ProjectSimple.Application.Helpers.PaginationBuilder;

namespace ProjectSimple.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly DefaultContext _dbContext;

    public GenericRepository(DefaultContext dbcontext)
    {
        _dbContext = dbcontext;
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T> DeleteAsync(T entity)
    {
        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T> GetAsync(long Id)
    {
        return await _dbContext.Set<T>()
                .AsNoTracking()
                .FirstAsync(x => x.Id == Id);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }
}