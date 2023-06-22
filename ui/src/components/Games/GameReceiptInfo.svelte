<style>
  h2 {
    font-size: 2em;
    text-align: center;
    margin-bottom: 12px;
  }
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
  import { ReceiptClient, type BasicGameInfoDto, ReceiptDto } from "../../nlp-api";

  export let game: BasicGameInfoDto;

  let loading = true;
  let receipt: ReceiptDto = new ReceiptDto();
  let recDate: string;

  const refreshReceiptInfo = (_game: BasicGameInfoDto | undefined) => {
    if(!_game) return;

    loading = true;
    receipt = new ReceiptDto();
    recDate = '';

    new ReceiptClient().getOrderInformation(_game.receiptID)
      .then((_receiptInfo: ReceiptDto) => {
        if(!_receiptInfo) return;
        receipt = _receiptInfo;
        recDate = receipt?.receiptDate.toISOString().split('T')[0] || '';
      })
      .finally(() => {
        loading = false;
      });
  };

  $: refreshReceiptInfo(game);
</script>

<div>
  {#if !game}
    Please select a game first.
  {:else}
      {#if loading}
        Loading...
      {:else}
        <h2>{game.gameName}</h2>
        <div class="form">
          <div class="row">
            <div class="field">
              <label for="store">Store</label>
              <input type="text" id="store" bind:value={receipt.store}>
            </div>
            <div class="field">
              <label for="order">Order</label>
              <input type="text" id="order" bind:value={receipt.receiptNumber}>
            </div>
            <div class="field">
              <label for="date">Date</label>
              <input type="date" id="date" bind:value={recDate}>
            </div>
          </div>

          <div class="row">
            <div class="field">
              <label for="name">Name</label>
              <input type="text" id="name" bind:value={receipt.receiptName}>
            </div>
            <div class="field">
              <label for="url">URL</label>
              <input type="text" id="url" bind:value={receipt.receiptUrl}>
            </div>
          </div>

          <div class="row">
            <div class="field">
              <label for="scanned">Scanned</label>
              <input type="checkbox" id="scanned" bind:value={receipt.receiptScanned}>
              <div class="spacer"></div>
            </div>
          </div>

          <div class="row">
            <button disabled>Save Changes</button>
          </div>
        </div>
      {/if}
  {/if}
</div>


