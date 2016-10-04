import {Component} from 'angular2/core';
import {IONIC_DIRECTIVES} from 'ionic-angular';

@Component({
  selector: 'homejoined',
  templateUrl: 'build/components/homejoined/homejoined.html',
  inputs: ['feedinfo'],
  directives: [IONIC_DIRECTIVES]
})
export class Homejoined {
  constructor() {
  }

  // Handle the Challege action from the slider
  handleChallenge(feedinfo) {
    console.log('handleChallenge');
  }

  // Handle the Friend action from the slider
  handleFriend(feedinfo) {
    console.log('handleFriend');
  }
}
