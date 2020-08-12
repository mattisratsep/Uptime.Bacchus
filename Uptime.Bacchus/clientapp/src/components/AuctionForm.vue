<template>
    <div>

        <b-alert v-model="bidSuccess"
                 class="position-fixed fixed-top m-0 rounded-0"
                 style="z-index: 2000;"
                 variant="success"
                 dismissible>
            Your bet has been submitted!
        </b-alert>

        <b-alert v-model="bidError"
                 class="position-fixed fixed-top m-0 rounded-0"
                 style="z-index: 2000;"
                 variant="danger"
                 dismissible>
            There was error in submiting your bet. Try again!
        </b-alert>

        <b-button block variant="secondary"
                  :class="visible ? null : 'collapsed'"
                  aria-controls="collapse-2"
                  :aria-expanded="visible ? 'true' : 'false'"
                  @click="collapseForm">
            {{this.collapseButtonText}}
        </b-button>

        <b-collapse id="collapse-2" v-model="visible" class="mt-2">

            <div class="text-center">
                <b-form-group :invalid-feedback="invalidFirstName"
                              :state="firstNameState">
                    <b-form-input placeholder="First name" v-model="firstName" type="text" trim></b-form-input>
                </b-form-group>

                <b-form-group :invalid-feedback="invalidLastName"
                              :state="lastNameState">
                    <b-form-input placeholder="Last name" v-model="lastName" type="text" trim></b-form-input>
                </b-form-group>

                <b-form-group :invalid-feedback="invalidAmount"
                              :state="amountState">
                    <b-form-input v-model="amount" type="number" trim></b-form-input>
                </b-form-group>

                <b-button block variant="secondary"
                          :disabled="enterButtonIsDisabled"
                          @click="submitBid(productId)">
                    Submit your bet
                </b-button>
            </div>

        </b-collapse>
    </div>
</template>

<script>
    import store from '../store';

export default {
        props: {
            productId: String
        },

        computed: {

            firstNameState() {
                return this.firstName.length >= 1 ? true : false
            },
            lastNameState() {
                return this.lastName.length >= 1 ? true : false
            },
            amountState() {
                return this.amount > 0 ? true : false
            },

            enterButtonIsDisabled() {

                if (this.firstName.length < 1) {
                    return true;
                } 
                if (this.lastName.length < 1 ) {
                    return true;
                }
                if (this.amount < 1) {
                    return true;
                }
                return false;
            },

            invalidAmount() {

                if (this.amount > 1) {
                    return ''
                }
                else {
                    return 'Amount must be greater than 0'
                }
            },
            invalidLastName() {

                if (this.lastName.length > 1) {
                    return ''
                }
                else {
                    return 'Please enter last name'
                }
            },
            invalidFirstName() {

                if (this.firstName.length > 1) {
                    return ''
                }
                else {
                    return 'Please enter first name'
                }
            }
        },
        data() {
            return {
                lastName: '',
                firstName: '',
                amount: 0,
                visible: false,
                bidSuccess: false,
                bidError: false,
                collapseButtonText: 'Make a bet'
            }

        },

        methods: {

            async submitBid() {

                const bet = {
                    bet: parseInt(this.amount),
                    firstName: this.firstName,
                    lastName: this.lastName,
                    productId: this.productId,
                    betTimestampMs: new Date(Date.now()).getTime()
                };

                try {

                    await store.methods.enterBet(bet);
                    this.bidSuccess = true;

                } catch (error) {

                    this.bidError = true;

                } finally {

                    this.lastName = '';
                    this.firstName = '';
                    this.amount = 0;
                    this.collapseForm();
                }         
            
            },

            collapseForm() {

                this.visible = !this.visible;

                if (this.visible === false) {
                    this.collapseButtonText = 'Make a bet';
                }
                else {
                    this.collapseButtonText = 'Close';
                }
           
            }

        }
}

</script>