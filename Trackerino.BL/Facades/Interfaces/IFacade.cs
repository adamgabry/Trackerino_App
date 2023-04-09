﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trackerino.BL.Models;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Facades;

public interface IFacade<TEntity, TListModel, TDetailModel>
    where TEntity : class, IEntity
    where TListModel : IModel
    where TDetailModel : class, IModel
{
    Task DeleteAsync(Guid id);
    Task<TDetailModel?> GetAsync(Guid id);
    Task<IEnumerable<TListModel>> GetAsync();
    Task<TDetailModel> SaveAsync(TDetailModel model);
}
