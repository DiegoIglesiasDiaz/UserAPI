﻿using Microsoft.EntityFrameworkCore;
using Domain.Models;
namespace Infrastructure;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
}
