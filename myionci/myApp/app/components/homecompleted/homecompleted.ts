import {Component} from 'angular2/core';
import {IONIC_DIRECTIVES} from 'ionic-angular';

@Component({
  selector: 'homecompleted',
  templateUrl: 'build/components/homecompleted/homecompleted.html',
  inputs: ['feedinfo'],
  directives: [IONIC_DIRECTIVES]
})
export class Homecompleted {
  constructor() {
  }

  // Handle the Challenge Winner action from the slider
  handleChallengeWinner(feedinfo) {
    console.log('handleChallengeWinner');
  }

  // Handle the Challenge Loser action from the slider
  handleChallengeLoser(feedinfo) {
    console.log('handleChallengeLoser');
  }
}
