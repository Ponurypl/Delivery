using MultiProject.Delivery.Domain.Webhooks.Entities;

namespace MultiProject.Delivery.Application.Common.Persistence.Repositories;
public interface IWebhookRepository
{
    void Add(Webhook webhook);
}
