import {Component} from 'angular2/core';
import {IONIC_DIRECTIVES} from 'ionic-angular';

@Component({
  selector: 'rewardsinfobar',
  templateUrl: 'build/components/rewardsinfobar/rewardsinfobar.html',
  inputs: ['userrewardsinfo'],
  directives: [IONIC_DIRECTIVES]
})
export class Rewardsinfobar {
  constructor() {
  }

}
