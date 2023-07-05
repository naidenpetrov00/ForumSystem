namespace ForumSystem.Services.Tests
{
    using ForumSystem.Data;
    using ForumSystem.Data.Models;
    using ForumSystem.Data.Repositories;
    using ForumSystem.Services.Data;
    using Microsoft.EntityFrameworkCore;

    public class VoteServiceTests
    {
        [Fact]
        public async Task TwoDownVotesShouldCountOnce()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfRepository<Vote>(
                new ApplicationDbContext(options.Options));
            var service = new VotesService(repository);

            await service.VoteAsync(1, false, "1", true);
            await service.VoteAsync(1, false, "1", true);
            var votesLoggedUser = service.GetVotes(1);

            await service.VoteAsync(2, false, "2", false);
            await service.VoteAsync(2, false, "2", false);
            var votesGuestUser = service.GetVotes(2);

            Assert.Equal(-1, votesLoggedUser);
            Assert.Equal(-1, votesGuestUser);
        }
    }
}