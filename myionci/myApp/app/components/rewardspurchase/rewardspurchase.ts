import {Component} from 'angular2/core';
import {IONIC_DIRECTIVES} from 'ionic-angular';

@Component({
  selector: 'rewardspurchase',
  templateUrl: 'build/components/rewardspurchase/rewardspurchase.html',
  inputs: ['feedinfo'],
  directives: [IONIC_DIRECTIVES]
})
export class rewardsPurchase {
  constructor() {
  }

  // Handle the Buy button
  handleBuy(feedinfo) {
    console.log('handleBuy');
  }

}
