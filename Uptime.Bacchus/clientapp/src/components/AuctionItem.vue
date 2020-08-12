<template>
    <div style="margin-top: 10px;">
        <b-card :header="item.productName" align="center" style=" max-width: 250px; height: 100%; margin-left: 10px;" class="mb-2">
            <b-card-body>
                    <b-card-sub-title class="mb-2">{{item.productCategory}}</b-card-sub-title>
                    <b-card-text>
                        {{ item.productDescription }}
                    </b-card-text>
                    <circular-count-down-timer :initial-value="Math.floor(new Date(item.biddingEndDate).getTime() / 1000) - Math.floor(new Date(Date.now()).getTime()/ 1000)"
                                                :stroke-width="3"
                                                :seconds-stroke-color="'#42b3f5'"
                                                :minutes-stroke-color="'#55cc2d'"
                                                :hours-stroke-color="'#e08722'"
                                                :underneath-stroke-color="'lightgrey'"
                                                :seconds-fill-color="'#62c4fc'"
                                                :minutes-fill-color="'#7be359'"
                                                :hours-fill-color="'#fca747'"
                                                :size="70"
                                                :padding="4"
                                                :hour-label="'hours'"
                                                :minute-label="'minutes'"
                                                :second-label="'seconds'"
                                                :show-second="true"
                                                :show-minute="true"
                                                :show-hour="true"
                                                :show-negatives="false"
                                                @finish="finished"></circular-count-down-timer>
            </b-card-body>

            <template v-slot:footer>
                <auction-form :productId="item.productId" />
            </template>

        </b-card>
    </div>
</template>
<script>
    import store from '../store';
    import AuctionForm from './AuctionForm.vue'

export default {
    components: {
        AuctionForm
    },
    props: {
        item: Object,
    },
    data: () => ({
        store: store.methods,
    }),

    methods: {
        finished: function () {
            
            this.itemAuctionFinished(this.item.productId);
        },

        itemAuctionFinished(productId) {
            store.methods.auctionFinished(productId);
        }

        
    }
}

</script>

