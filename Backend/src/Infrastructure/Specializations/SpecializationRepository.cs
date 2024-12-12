using Backend.Domain.Specializations;
using Backend.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Specializations;

public class SpecializationRepository : BaseRepository<Specialization, SpecializationID>, ISpecializationRepository {

    public SpecializationRepository(AppDbContext context) : base(context.Specializations) {
        
    }

    public SpecializationRepository(DbSet<Specialization> objs) : base(objs) {
        
    }
    
}