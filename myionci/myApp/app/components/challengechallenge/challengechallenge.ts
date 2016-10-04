import {Component} from 'angular2/core';
import {IONIC_DIRECTIVES} from 'ionic-angular';

@Component({
  selector: 'challengechallenge',
  templateUrl: 'build/components/challengechallenge/challengechallenge.html',
  inputs: ['feedinfo'],
  directives: [IONIC_DIRECTIVES]
})
export class Challengechallenge {
  constructor() {
  }

  // Handle the New Challenge Back action from the slider
  handleAcceptAndCounter(feedinfo) {
    console.log('handleAcceptAndCounter');
  }

  // Handle the Reject action from the slider
  handleReject(feedinfo) {
    console.log('handleReject');
  }

  // Handle the Accept action from the slider
  handleAccept(feedinfo) {
    console.log('handleAccept');
  }
}
