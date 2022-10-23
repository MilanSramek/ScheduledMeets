export class Path {
  private parent: Path | null;
  public value: string | null;

  constructor(parent: Path | null, value: string | null) {
    this.parent = parent;
    this.value = value;
  }

  get totalPath(): string {
    return `${this.parent ?? ''}/${this.value ?? ''}`;
  }

  get [Symbol.toStringTag]() {
    return this.value;
  }
}
