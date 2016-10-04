import {Page, NavController, NavParams} from 'ionic-angular';
import {Validators, FormBuilder, FORM_DIRECTIVES} from 'angular2/common';
// Data providers
import {ActiveChallengeFeed} from '../../providers/activechallengefeed/activechallengefeed';

@Page({
  templateUrl: 'build/pages/activechallenge/activechallenge.html',
  providers: [ActiveChallengeFeed]
})
export class ActiveChallengePage {


  public challengeinfo: any;          // All challenge info
  public thisuser: any;          // All challenge info
  public challenger: any;          // All challenge info


  constructor(private nav: NavController, private activeChallengeFeed: ActiveChallengeFeed) {

  	this.challengeinfo = activeChallengeFeed.loadResults();

  	// get user info for easier logic in html
  	this.thisuser = this.challengeinfo.thisuser;
  	this.challenger = this.challengeinfo.challenger;


  	console.log(this.challengeinfo);

  }

  handleUpdateResults() {
  	console.log('handle update results');
  }

  handleReward() {
  	console.log('handle reward');
  }

}