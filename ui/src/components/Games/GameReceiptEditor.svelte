<script lang="ts">
	import { GameReceiptClient, type GameReceiptDto } from "../../nlp-api";

  export let receipt: GameReceiptDto;
  export let onReceiptChanged: (receipt: GameReceiptDto) => void;
  let recDate = '';
  let formChanged = false;

  const setReceiptDate = (rec: GameReceiptDto | undefined) => {
    if(!rec) {
      recDate = new Date().toISOString().split('T')[0];
      return;
    }

    if(rec.receiptDate < new Date(2000,0,1)) {
      recDate = new Date().toISOString().split('T')[0];
      return;
    }

    recDate = rec.receiptDate.toISOString().split('T')[0];
  };

  const saveChanges = () => {
    formChanged = false;
    const dateParts = recDate.split('-');
    receipt.receiptDate = new Date(parseInt(dateParts[0]), parseInt(dateParts[1])-1, parseInt(dateParts[2]));
    new GameReceiptClient().updateReceipt(receipt)
      .then((_response: GameReceiptDto) => {
        if(!_response) {
          alert('Failed to save changes!');
          return;
        }
        onReceiptChanged(_response);
      });
  };

  const valueChanged = () => formChanged = true;

  $: setReceiptDate(receipt);
</script>

<form class="row g-3">
  <div class="col-md-4">
    <label for="store" class="form-label">Store</label>
    <input type="text" class="form-control" id="store" bind:value={receipt.store} on:keyup={valueChanged}>
  </div>
  <div class="col-md-4">
    <label for="order" class="form-label">Order</label>
    <input type="text" class="form-control" id="order" bind:value={receipt.receiptNumber} on:keyup={valueChanged}>
  </div>
  <div class="col-md-4">
    <label for="date" class="form-label">Date</label>
    <input type="date" class="form-control" id="date" bind:value={recDate} on:keyup={valueChanged} on:change={valueChanged}>
  </div>
  <div class="col-6">
    <label for="name" class="form-label">Name</label>
    <input type="text" class="form-control" id="name" bind:value={receipt.receiptName} on:keyup={valueChanged}>
  </div>
  <div class="col-6">
    <label for="url" class="form-label">URL</label>
    <input type="text" class="form-control" id="url" bind:value={receipt.receiptUrl} on:keyup={valueChanged}>
  </div>
  <div class="col-6">
    <div class="form-check form-switch form-check-inline">
      <input class="form-check-input" type="checkbox" role="switch" id="scanned" bind:checked={receipt.receiptScanned} on:change={valueChanged}>
      <label class="form-check-label" for="scanned">Scanned</label>
    </div>
  </div>
  <button type="button" class="btn btn-primary" disabled={!formChanged} on:click={saveChanges}>Save Changes</button>
</form>