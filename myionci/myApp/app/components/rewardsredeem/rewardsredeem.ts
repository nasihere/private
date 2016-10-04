import {Component} from 'angular2/core';
import {IONIC_DIRECTIVES} from 'ionic-angular';

@Component({
  selector: 'rewardsredeem',
  templateUrl: 'build/components/rewardsredeem/rewardsredeem.html',
  inputs: ['feedinfo'],
  directives: [IONIC_DIRECTIVES]
})
export class rewardsRedeem {
  constructor() {
  }

  // Handle the Buy button
  handleBuy(feedinfo) {
    console.log('handleBuy');
  }

}
