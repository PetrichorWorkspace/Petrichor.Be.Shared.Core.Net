using Microsoft.EntityFrameworkCore;

namespace Shared.Core.Driven.Persistence.EFCore;

// TODO do we need it?
public abstract class PersistenceCtx(DbContextOptions options) : DbContext(options);