import {Component} from 'angular2/core';
import {IONIC_DIRECTIVES} from 'ionic-angular';

@Component({
  selector: 'homelost',
  templateUrl: 'build/components/homelost/homelost.html',
  inputs: ['feedinfo', 'userprofile'],
  directives: [IONIC_DIRECTIVES]
})
export class Homelost {
  constructor() {
  }

  // Handle the Challenge Again action from the slider
  handleChallengeAgain(feedinfo) {
    console.log('handleChallengeAgain');
  }

  // Handle the Socialize action from the slider
  handleSocialize(feedinfo) {
    console.log('handleSocialize');
  }

}
