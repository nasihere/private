import {Component} from 'angular2/core';
import {IONIC_DIRECTIVES} from 'ionic-angular';

@Component({
  selector: 'challengewon',
  templateUrl: 'build/components/challengewon/challengewon.html',
  inputs: ['feedinfo', 'userprofile'],
  directives: [IONIC_DIRECTIVES]
})
export class Challengewon {
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