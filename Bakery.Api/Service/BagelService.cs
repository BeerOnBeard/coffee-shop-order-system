using System.Collections.Generic;
using System.Linq;
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

    public IEnumerable<Bagel> Get()
    {
      return _repository.Get().Where(b => !b.IsComplete);
    }

    public void Complete(Bagel bagel)
    {
      bagel.IsComplete = true;
      _publisher.PublishCompleted(bagel);
    }
  }
}