import {Page, NavController, NavParams, Keyboard, Platform} from 'ionic-angular';
import {Validators, FormBuilder} from 'angular2/common';
import {HistoryPage} from '../history/history';
//data providers
import {Rewardsfeed} from '../../providers/rewardsfeed/rewardsfeed';
// Components for the view
import {rewardsPurchase} from '../../components/rewardspurchase/rewardspurchase';
import {rewardsRedeem} from '../../components/rewardsredeem/rewardsredeem';
import {Rewardsinfobar} from '../../components/rewardsinfobar/rewardsinfobar';

// Declare variable used for checkout
declare var StripeCheckout: any;

@Page({
  templateUrl: 'build/pages/tab-rewards/tab-rewards.html',
  directives: [rewardsPurchase, rewardsRedeem, Rewardsinfobar],
  providers: [Rewardsfeed]
})
export class TabRewardsPage {
  // Definitions
  public giftPointsForm: any;               // Gitt Verification form info.
  public allrewards: any;                   // Feed results
  public purchaserewards: any;              // Rewards list
  public redeemrewards: any;                // Redeem list
  public keyword: string = '';              // Search value
  public hideSearchbar: boolean = true;     // Flag to hide/show search
  public searchInput: any;                  // Search field
  public rewardtype: string = "purchase";   // Default tab
  public isAndroid: any;                    // Platform flag
  public userrewardsinfo: any;              // User Info results
  public handler: any;                      // To handle checkout

  constructor(private nav: NavController,
              private rewardsFeed: Rewardsfeed,
              private keyboard:Keyboard,
              platform: Platform,
              fb: FormBuilder) {
    console.log("tab-rewards page");

    // Setup
    this.isAndroid = platform.is('android');
    this.allrewards = [];
    this.purchaserewards = [];
    this.redeemrewards = [];

    // Get the data for the User Rewards Info
    this.userrewardsinfo = rewardsFeed.loadUserStatus();

    // Get the data for the Challenges Feed
    this.allrewards = rewardsFeed.loadResults();

    // Organize the results into Groupings
    this.allrewards.forEach(function (reward) {
      switch (reward.rewardtype) {
        case "purchase":
          this.purchaserewards.push(reward);
        break;
        case "redeem":
          this.redeemrewards.push(reward);
        break;
      }
    }, this);

    // Setup gifting form
    this.giftPointsForm = fb.group({
      podiimpoints: ['', Validators.required],
      points: ['', Validators.required]
    });


    // Handle checkout
    this.handler = StripeCheckout.configure({
      key: 'pk_test_6pRNASCoBOKtIshFeQd4XMUh',
      image: '/img/documentation/checkout/marketplace.png',
      locale: 'auto',
      token: function(token) {
        // You can access the token ID with token.id.
        // Get the token ID to your server-side code for use.
      }
    });
  }

  // Open checkout form
  checkout() {
     this.handler.open({
     name: 'Stripe.com',
     description: '2 widgets',
     amount: 2000
   });
  }

  // Handle selecting the person to Gift to
  handleSelectPerson() {
    console.log("handleSelectPerson");
  }

  // Handle removing the person to Gift to
  handleRemovePerson() {
    console.log("handleRemovePerson");
  }

  // Handle Gifting to the person
  handleGifting() {
    console.log("handleGifting");
  }

  // Handle showing the Rewards History
  handleRewardHistory() {
    console.log("handleRewardHistory");
    this.nav.push(HistoryPage);
  }
  // SEARCHBAR FUNCTIONALITY - START

  // show and hide the searchbar
  toggleSearchbar() {
      console.log('toggleSearchbar');
      // Toggle the state
      this.hideSearchbar = !this.hideSearchbar;
      // Show the search bar if necessary
      if (!(this.hideSearchbar)) {
          // focus searchbar input after it is displayed
          setTimeout(() => {
              // Need to grab the input element in ion-searchbar to focus it
              this.searchInput = document.getElementById('searchBar').getElementsByTagName('div')[0].getElementsByTagName('input')[0];
              this.searchInput.focus();
          }, 0);
      }
  }

  // Handle the actual Search query here
  search() {
      console.log('search function called');
  }

  // Handle the Search cancel
  userPressedCancel() {
      console.debug('User pressed cancel');
  }

  // Call the search function when the user hits enter and the query isn't blank
  keyHasBeenPressed(e) {
      this.keyboard.close();
      if(this.keyword !== '') {
        this.search();
      }
  }

}