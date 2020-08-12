<template>
    <b-container>
        <b-row>
            <b-list-group horizontal>
                <b-list-group-item v-for="category in categories"
                                   @click="addFilter(category)" 
                                   :key="category"  
                                   :active="filters.includes(category)"  
                                   button variant="light">{{category}}</b-list-group-item>
            </b-list-group>
            <b-button @click="clearFilter"
                      squared variant="outline-info" 
                      style="margin-left: 10px;">
            Clear filter
            </b-button>
        </b-row>

        <b-row>
            <auction-item v-for="item in loadedItems" :key="item.productId" :item="item" />
        </b-row>
    </b-container>  
</template>

<script>

    import store from '../store';
    import AuctionItem from './AuctionItem.vue'
export default {
    components: {
        AuctionItem
    },
    data: () => ({
        store: store.data,
        filters:[],
        auctionItems:[]
        }),

    methods: {

        clearFilter() {
                this.filters = [];
        },

        addFilter(category) {
            if (!this.filters.includes(category)) {
                this.filters.push(category);
            }
            else {
                const index = this.filters.indexOf(category);
                if (index > -1) {
                    this.filters.splice(index, 1);
                }
            }
            
        }
    },

    computed: {

        loadedItems() {

            if (!this.store.auctionItems) {
                return [];
            }
            let categoryFilter = this.filters;
            let filtered = this.store.auctionItems;

            if (categoryFilter.length > 0) {
                let filterArray = []
                for (var i = 0; i < categoryFilter.length; i++) {

                    filterArray.push(...this.store.auctionItems.filter(r => r.productCategory === categoryFilter[i]));
                }
                filtered = filterArray;
            }
            
            return filtered;
        },

        categories() {
            if (!this.store.categories) {
                return [];
            }

            return this.store.categories;
        }

    },

    mounted() {
        store.methods.loadAuctionItems();
    }
}

</script>