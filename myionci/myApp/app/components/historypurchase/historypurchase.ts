import {Component} from 'angular2/core';
import {IONIC_DIRECTIVES} from 'ionic-angular';

@Component({
  selector: 'historypurchase',
  templateUrl: 'build/components/historypurchase/historypurchase.html',
  inputs: ['feedinfo'],
  directives: [IONIC_DIRECTIVES]
})
export class Historypurchase {
  constructor() {
  }

}