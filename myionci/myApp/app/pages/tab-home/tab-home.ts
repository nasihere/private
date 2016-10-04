import {Page, Tabs, NavController, NavParams, Keyboard, Alert, IonicApp} from 'ionic-angular';
// Data providers
import {Homefeed} from '../../providers/homefeed/homefeed';
import {Userprofile} from '../../providers/userprofile/userprofile';
// Components for the view
import {Homechallenge} from '../../components/homechallenge/homechallenge';
import {Homecompleted} from '../../components/homecompleted/homecompleted';
import {Homejoined} from '../../components/homejoined/homejoined';
import {Homelost} from '../../components/homelost/homelost';
import {Homewon} from '../../components/homewon/homewon';
import {Homeinfobar} from '../../components/homeinfobar/homeinfobar';

@Page({
  templateUrl: 'build/pages/tab-home/tab-home.html',
  directives: [Homechallenge, Homecompleted, Homejoined, Homelost, Homewon, Homeinfobar],
  providers: [Homefeed, Userprofile]
})
export class TabHomePage {
    // Definitions
    public results: any;                    // Feed results
    public userprofile: any;                // User profile info.
    public keyword: string = '';            // Search value
    public hideSearchbar: boolean = true;   // Flag to hide/show search
    public searchInput: any;                // Search field
    public tabposition: number = 0;         // Home tab position
    public notificationcount: number = 0;   // Number of notifications pending

    constructor(private nav: NavController,
                private homeFeed: Homefeed,
                private userProfile: Userprofile,
                private keyboard:Keyboard,
                private app: IonicApp) {
        console.log("tab-home page");

        // Get the data for the User Profile
        this.userprofile = userProfile.loadProfile();

        // Get the data for the Home Feed
        this.results = this.getHomeFeed();

        // Handle setting the badge notifications
        this.setHomeFeedBadgeCount(this.results);
    }

    // Handle getting the Feed data
    getHomeFeed() {
        return this.homeFeed.loadResults();
    }

    // Handle setting the badge count from the feed
    setHomeFeedBadgeCount(feed) {
        // Setup
        var notifications = '';
        // Get the tab to update
        let tabs = this.app.getComponent('navtabs');
        let tab = tabs.getByIndex(this.tabposition);
        // Loop through the feed to get any notifications
        this.notificationcount = 0;
        feed.forEach(function (challenge) {
            if (challenge.notification) {
                this.notificationcount++;
            }
        });
        // Update the badge count
        if (this.notificationcount > 0) {
            notifications = String(this.notificationcount);
        };
        tab.tabBadge = notifications;
    }

    // Handle the show logged in users profile
    handleShowMyProfile() {
        console.log('handleShowMyProfile');
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