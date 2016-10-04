import {Component} from 'angular2/core';
import {IONIC_DIRECTIVES} from 'ionic-angular';

@Component({
  selector: 'historygifted',
  templateUrl: 'build/components/historygifted/historygifted.html',
  inputs: ['feedinfo'],
  directives: [IONIC_DIRECTIVES]
})
export class Historygifted {
  constructor() {
  }

  // Handle the Challenge action from the slider
  handleChallenge(feedinfo) {
    console.log('handleChallenge');
  }
}
