import {Page, NavController, NavParams} from 'ionic-angular';
import {Validators, FormBuilder, FORM_DIRECTIVES} from 'angular2/common';
import {MoreShared} from '../moreshared/moreshared';
import {ActiveChallengePage} from '../activechallenge/activechallenge';

// Data providers
import {Peoplefeed} from '../../providers/peoplefeed/peoplefeed';
// Components for the view
import {chooseChallenger} from '../../components/choosechallenger/choosechallenger';


@Page({
  templateUrl: 'build/pages/createchallenge/createchallenge.html',
  directives: [FORM_DIRECTIVES, chooseChallenger],
  providers: [Peoplefeed, MoreShared]
})
export class CreateChallengePage {
  // Definitions
  public challengeform: any;        // Challenge form info.
  public everyperson: any;          // All people list
  public chosenpeople: any;          // All people list
  public readterms: any;          // All people list
  public moreshared: any;

  constructor(private nav: NavController, fb: FormBuilder, private peopleFeed: Peoplefeed, moreshared: MoreShared) {
    this.challengeform = fb.group({
      'name': ['', Validators.required],
      'purpose': ['health', Validators.required],
      'timeframe': ['1day', Validators.required],
      'startdate': ['', Validators.required],
      'challengetype': ['1v1', Validators.required],
      'category': ['running', Validators.required],
      'challengecourse': ['distance', Validators.required],
      'description': ['', Validators.required],
      'proof': ['uploadpic', Validators.required],
      'proofdescription': [''],
      'privacy': ['private', Validators.required],
      'reward': ['100P', Validators.required],
      'socialize': ['none'],
      'readterms': [false, Validators.required]
    });

    // Get all friends
    this.everyperson = peopleFeed.loadResults();

    // Every person starts out unselected
    this.everyperson.forEach( function(person){
      person.chosen = false;
    });
    this.chosenpeople = [];

    this.moreshared = moreshared;

  }

  onSubmit(value: string): void {

    this.chosenpeople = [];
    // Make new array of all selected people
    this.everyperson.forEach(function (person) {
      switch (person.chosen) {
        case true:
          this.chosenpeople.push(person);
        break;
     }
    }, this);
    console.log(this.chosenpeople);

    // submit if terms are agreed to and form is valid
    if(!value['readterms']){
      console.log('you must agree to safety terms');
    }
    else if(this.challengeform.valid){
      console.log('you submitted value: ', value);
    }
    else{
      console.log('form is not valid');
    }
  }

  // Handle going to the Savety Terms view
  openSafetyTerms() {
    this.moreshared.openSafetyTerms();
  }

  // Handle going to the Login view
  openActiveChallengePage() {
    this.nav.push(ActiveChallengePage);
  }

}