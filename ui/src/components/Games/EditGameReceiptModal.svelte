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

<form>
  <div class="flex">
    <div class="form-control w-full mr-2">
      <label class="label" for="store">
        <span class="label-text">Store</span>
      </label>
      <input type="text" class="input input-bordered w-full" id="store" bind:value={receipt.store} on:keyup={valueChanged}>
    </div>
    <div class="form-control w-full">
      <label class="label" for="name">
        <span class="label-text">Receipt Name</span>
      </label>
      <input type="text" class="input input-bordered w-full" id="name" bind:value={receipt.receiptName} on:keyup={valueChanged}>
    </div>
  </div>
  <div class="flex">
    <div class="form-control w-full mr-2">
      <label class="label" for="order">
        <span class="label-text">Order</span>
      </label>
      <input type="text" class="input input-bordered w-full" id="order" bind:value={receipt.receiptNumber} on:keyup={valueChanged}>
    </div>
    <div class="form-control w-full">
      <label class="label" for="date">
        <span class="label-text">Date</span>
      </label>
      <input type="date" class="input input-bordered w-full" id="date" bind:value={recDate} on:keyup={valueChanged} on:change={valueChanged}>
    </div>
  </div>
  <div class="form-control w-full">
    <label class="label" for="url">
      <span class="label-text">URL</span>
    </label>
    <input type="text" class="input input-bordered w-full" id="url" bind:value={receipt.receiptUrl} on:keyup={valueChanged}>
  </div>
  <div class="form-control">
    <label class="label cursor-pointer">
      <span class="label-text">Scanned</span> 
      <input type="checkbox" class="toggle" id="hasBox" bind:checked={receipt.receiptScanned} on:change={valueChanged} />
    </label>
  </div>
  <button type="button" class="btn btn-primary w-full" disabled={!formChanged} on:click={saveChanges}>Save Changes</button>
</form>