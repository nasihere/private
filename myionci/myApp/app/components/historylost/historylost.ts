import {Component} from 'angular2/core';
import {IONIC_DIRECTIVES} from 'ionic-angular';

@Component({
  selector: 'historylost',
  templateUrl: 'build/components/historylost/historylost.html',
  inputs: ['feedinfo'],
  directives: [IONIC_DIRECTIVES]
})
export class Historylost {
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