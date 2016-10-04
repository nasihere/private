import {Page, NavController, NavParams, Keyboard} from 'ionic-angular';
// Data providers
import {Peoplefeed} from '../../providers/peoplefeed/peoplefeed';
// Components for the view
import {peopleFollowers} from '../../components/peoplefollowers/peoplefollowers';
import {peopleFollowing} from '../../components/peoplefollowing/peoplefollowing';
import {peopleSuggested} from '../../components/peoplesuggested/peoplesuggested';

@Page({
  templateUrl: 'build/pages/tab-people/tab-people.html',
  directives: [peopleFollowers, peopleFollowing, peopleSuggested],
  providers: [Peoplefeed]
})
export class TabPeoplePage {
	// Definitions
	public peopletype: string = "following";	// Tab choice
	public keyword: string = '';            	// Search value
	public hideSearchbar: boolean = true;			// Flag to hide/show search
	public searchInput: any;									// Search field
	public everyperson: any;									// All people list
	public following: any;										// Following list
	public followers: any;										// Followers list
	public suggested: any;										// Suggested list

  constructor(private nav: NavController, private keyboard: Keyboard, private peopleFeed: Peoplefeed ) {
    console.log("tab-people page");

		// Setup
    this.everyperson = [];
    this.following = [];
    this.followers = [];
    this.suggested = [];

    // Get the data for the Challenges Feed
    this.everyperson = peopleFeed.loadResults();

    // Organize the results into Groupings
    this.everyperson.forEach(function (person) {
      switch (person.type) {
        case "following":
          this.following.push(person);
        break;
        case "followers":
          this.followers.push(person);
        break;
        case "suggested":
          this.suggested.push(person);
        break;      }
    }, this);

  }

	// Handle a new invitation
	handleInvite() {
		console.log("handleInvite");
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