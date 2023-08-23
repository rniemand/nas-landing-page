import type { ContainerDto } from "../../nlp-api";

const alpha = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';

export class ContainerHelper {
  public static generateLabel = (container: ContainerDto) => {
    return `${container.shelfNumber}${alpha[container.shelfLevel - 1]}:${container.shelfRow}-${container.shelfRowPosition}`;
  };
};

export class SearchResult {
  constructor(
    public title: string,
    public value: any = undefined,
    public description: string = ''
  ) {
    if(!this.value) this.value = this.title;
  }
};
