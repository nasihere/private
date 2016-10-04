import {Page, NavController, NavParams, Keyboard, Alert, Platform} from 'ionic-angular';
// Data providers
import {Challengesfeed} from '../../providers/challengesfeed/challengesfeed';
import {Userprofile} from '../../providers/userprofile/userprofile';
// Components for the view
import {Challengechallenge} from '../../components/challengechallenge/challengechallenge';
import {Challengecompleted} from '../../components/challengecompleted/challengecompleted';
import {Challengeinprogress} from '../../components/challengeinprogress/challengeinprogress';
import {Challengelost} from '../../components/challengelost/challengelost';
import {Challengewon} from '../../components/challengewon/challengewon';

@Page({
  templateUrl: 'build/pages/tab-challenges/tab-challenges.html',
  directives: [Challengechallenge, Challengecompleted, Challengeinprogress, Challengelost, Challengewon],
  providers: [Challengesfeed, Userprofile]
})
export class TabChallengesPage {
  // Definitions
  public allchallenges: any;
  public activechallenges: any;
  public openchallenges: any;
  public donechallenges: any;
  public userprofile: any;
  public keyword: string = '';
  public hideSearchbar: boolean = true;
  public searchInput: any;
  public isAndroid: boolean = false;
  public challengetype: string = "all";

  constructor(private nav: NavController, private challengesFeed: Challengesfeed, private userProfile: Userprofile, private keyboard:Keyboard, platform: Platform) {
    console.log("tab-challenges page");

    // Setup
    this.isAndroid = platform.is('android');
    this.allchallenges = [];
    this.activechallenges = [];
    this.openchallenges = [];
    this.donechallenges = [];

    // Get the data for the User Profile
    this.userprofile = userProfile.loadProfile();

    // Get the data for the Challenges Feed
    this.allchallenges = challengesFeed.loadResults();

    // Organize the results into Groupings
    this.allchallenges.forEach(function (challenge) {
      switch (challenge.challengestate) {
        case "active":
          this.activechallenges.push(challenge);
        break;
        case "open":
          this.openchallenges.push(challenge);
        break;
        case "done":
          this.donechallenges.push(challenge);
        break;
      }
    }, this);

   }

  // Handle the Create a New Challenge
  handleNewChallenge() {
    console.log('handleNewChallenge');
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