import Axios from 'axios';

const client = Axios.create({
    timeout: 120 * 1000,
    headers: {
        'Cache-Control': 'no-cache',
        'Pragma': 'no-cache',
        'Content-Type': 'application/json'
    },
});


const store = {

    data: {
        auctionItems: [],
        bidSuccess: false,
        bidError: false,
        categories:[]
    },
    methods: {
        
        loadAuctionItems() {

            client.get('/api/auction')
                .then(response => {
                    let responseData = response.data;

                    for (var i = 0; i < responseData.length; i++) {

                        if (!store.data.auctionItems.find(o => o.productId === responseData[i].productId)) {
                            store.data.auctionItems.push(responseData[i]);
                        }
                    }

                }).finally(() => this.assembleAuctionCategorys())

        },

        auctionFinished(productId) {

            for (var i = 0; i < store.data.auctionItems.length; i++) {

                if (store.data.auctionItems[i].productId === productId) {

                    store.data.auctionItems.splice(i, 1);
                }
            }
            this.loadAuctionItems();
        },
        async enterBet(auctionBet) {

            await client.post('/api/auction/makeBet', auctionBet);
        },

        assembleAuctionCategorys() {

            let filterStrings = [];

            for (var i = 0; i < store.data.auctionItems.length; i++) {
                if (!filterStrings.includes(store.data.auctionItems[i].productCategory)) {
                    filterStrings.push(store.data.auctionItems[i].productCategory)
                }
            }
            store.data.categories = filterStrings;

        },

    }
}

export default store;