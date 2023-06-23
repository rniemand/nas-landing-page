<style>
  .form .row {
    display: flex;
    justify-content: space-evenly;
    margin-bottom: 12px;
  }
  .form .field label {
    font-weight: bold;
    padding-top: 2px;
    margin-right: 4px;
  }
  .form .field {
    flex: auto;
    display: flex;
  }
  .form .field .spacer {
    flex: 120;
  }
  .form .field input {
    flex: auto;
    margin-right: 6px;
    padding: 2px;
    border-radius: 4px;
    border: 1px solid #b9b5b5;
  }
</style>

<script lang="ts">
	import { GameReceiptClient, type ReceiptDto } from "../../nlp-api";

  export let receipt: ReceiptDto;
  export let onReceiptChanged: (receipt: ReceiptDto) => void;
  let recDate = '';
  let formChanged = false;

  const setReceiptDate = (rec: ReceiptDto | undefined) => {
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
      .then((_response: ReceiptDto) => {
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

<div class="form">
  <div class="row">
    <div class="field">
      <label for="store">Store</label>
      <input type="text" id="store" bind:value={receipt.store} on:keyup={valueChanged}>
    </div>
    <div class="field">
      <label for="order">Order</label>
      <input type="text" id="order" bind:value={receipt.receiptNumber} on:keyup={valueChanged}>
    </div>
    <div class="field">
      <label for="date">Date</label>
      <input type="date" id="date" bind:value={recDate} on:keyup={valueChanged} on:change={valueChanged}>
    </div>
  </div>

  <div class="row">
    <div class="field">
      <label for="name">Name</label>
      <input type="text" id="name" bind:value={receipt.receiptName} on:keyup={valueChanged}>
    </div>
    <div class="field">
      <label for="url">URL</label>
      <input type="text" id="url" bind:value={receipt.receiptUrl} on:keyup={valueChanged}>
    </div>
  </div>

  <div class="row">
    <div class="field">
      <label for="scanned">Scanned</label>
      <input type="checkbox" id="scanned" bind:value={receipt.receiptScanned} on:change={valueChanged}>
      <div class="spacer"></div>
    </div>
  </div>

  <div class="row">
    <button disabled={!formChanged} on:click={saveChanges}>Save Changes</button>
  </div>
</div>