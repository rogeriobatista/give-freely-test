﻿namespace EmployeeManagementSystem.Infra.Data.UnityOfWork
{
    public interface IUnityOfWork
    {
        Task SaveChanges();
    }
}
