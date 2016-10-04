import {Component} from 'angular2/core';
import {IONIC_DIRECTIVES} from 'ionic-angular';

@Component({
  selector: 'homechallenge',
  templateUrl: 'build/components/homechallenge/homechallenge.html',
  inputs: ['feedinfo'],
  directives: [IONIC_DIRECTIVES]
})
export class Homechallenge {
  constructor() {
  }

  // Handle the Accept & Counter action from the slider
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
