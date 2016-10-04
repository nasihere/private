import {Component} from 'angular2/core';
import {IONIC_DIRECTIVES} from 'ionic-angular';

@Component({
  selector: 'challengelost',
  templateUrl: 'build/components/challengelost/challengelost.html',
  inputs: ['feedinfo', 'userprofile'],
  directives: [IONIC_DIRECTIVES]
})
export class Challengelost {
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
