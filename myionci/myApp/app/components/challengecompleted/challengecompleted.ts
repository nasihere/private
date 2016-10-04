import {Component} from 'angular2/core';
import {IONIC_DIRECTIVES} from 'ionic-angular';

@Component({
  selector: 'challengecompleted',
  templateUrl: 'build/components/challengecompleted/challengecompleted.html',
  inputs: ['feedinfo'],
  directives: [IONIC_DIRECTIVES]
})
export class Challengecompleted {
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
