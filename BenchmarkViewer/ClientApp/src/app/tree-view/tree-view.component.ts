import { Component, OnInit, Inject, EventEmitter, Output } from '@angular/core';
import { TreeviewItem, TreeviewConfig } from 'ngx-treeview';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-tree-view',
  templateUrl: './tree-view.component.html',
  styleUrls: ['./tree-view.component.css']
})
export class TreeViewComponent implements OnInit {
  items: TreeviewItem[];
  @Output() selectedChange = new EventEmitter<number[]>();

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<TreeViewNodeViewModel[]>(baseUrl + 'api/Benchmarks/').subscribe(result => {
      this.items = this.mapViewModel(result);
    }, error => console.error(error));
  }

  config = TreeviewConfig.create({
    hasAllCheckBox: true,
    hasFilter: true,
    hasCollapseExpand: true,
    decoupleChildFromParent: false,
    maxHeight: 800,
  });

  ngOnInit() {
  }

  mapViewModel(input: TreeViewNodeViewModel[]): TreeviewItem[] {
    return input.map(viewModel => new TreeviewItem(
      {
        text: viewModel.text,
        value: viewModel.id,
        children: this.mapViewModel(viewModel.children),
        checked: false,
        collapsed: true,
      }
    ));
  }

  onSelectedChange(selectedIds: number[]) {
    this.selectedChange.emit(selectedIds);
  }
}

interface TreeViewNodeViewModel {
  text: string;
  id: number;
  children: TreeViewNodeViewModel[];
}
