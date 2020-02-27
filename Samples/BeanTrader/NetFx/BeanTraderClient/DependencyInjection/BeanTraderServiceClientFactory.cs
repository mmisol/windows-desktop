using System.ServiceModel;

namespace BeanTraderClient.DependencyInjection
{
    public class BeanTraderServiceClientFactory
    {
        private BeanTraderServerNS.BeanTraderServiceCallback CallbackHandler { get; }

        public BeanTraderServiceClientFactory(BeanTraderServerNS.BeanTraderServiceCallback callbackHandler)
        {
            CallbackHandler = callbackHandler;
        }

        public BeanTraderServerNS.BeanTraderServiceClient GetServiceClient()
        {
            var client = new BeanTraderServerNS.BeanTraderServiceClient();
            client.AddNewTradeOfferReceived += Client_AddNewTradeOfferReceived;
            client.RemoveTradeOfferReceived += Client_RemoveTradeOfferReceived;
            client.TradeAcceptedReceived += Client_TradeAcceptedReceived;
            return client;
        }

        private void Client_TradeAcceptedReceived(object sender, BeanTraderServerNS.TradeAcceptedReceivedEventArgs e)
        {
            CallbackHandler.TradeAccepted(e.offer, e.buyerId);
        }

        private void Client_RemoveTradeOfferReceived(object sender, BeanTraderServerNS.RemoveTradeOfferReceivedEventArgs e)
        {
            CallbackHandler.RemoveTradeOffer(e.offerId);
        }

        private void Client_AddNewTradeOfferReceived(object sender, BeanTraderServerNS.AddNewTradeOfferReceivedEventArgs e)
        {
            CallbackHandler.AddNewTradeOffer(e.offer);
        }
    }
}
