using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakery.Api.Eventing;
using Bakery.Api.Models;
using Bakery.Api.Repository;

namespace Bakery.Api.Service
{
  public class BagelService : IBagelService
  {
    private readonly IBagelRepository _repository;
    private readonly IBagelEventPublisher _publisher;

    public BagelService(IBagelRepository repository, IBagelEventPublisher publisher)
    {
      _publisher = publisher;
      _repository = repository;
    }

    public async Task<IEnumerable<Bagel>> Get()
    {
      return await _repository.GetIncomplete();
    }

    public async Task Complete(Bagel bagel)
    {
      bagel.IsComplete = true;
      await _publisher.PublishCompleted(bagel);
    }
  }
}