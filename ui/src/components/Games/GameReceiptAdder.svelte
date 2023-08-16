<style>
  .add-btn { width: 120px; margin-right: 6px; }
  .name { flex: auto; text-align: left; }
  .number { text-align: center; width: 20%; color: rgb(74, 185, 250); }
  .store { text-align: right; width: 20%; color: rgb(9, 172, 49); }
  .select { margin-left: 4px; }
  .result span { margin: auto; }
  .result:hover { background-color: rgb(241, 241, 241); }
</style>

<script lang="ts">
  import { GameReceiptClient, type BasicGameInfoDto, GameReceiptDto } from "../../nlp-api";
  export let game: BasicGameInfoDto;
  export let onReceiptAdded: (receipt: GameReceiptDto) => void;
  let searchTerm = '';
  let receipts: GameReceiptDto[] = [];
  
  const searchReceipts = (term: string) => {
    if(term.length < 2) return;
    receipts = [];
    new GameReceiptClient().search(term).then((_results: GameReceiptDto[]) => receipts = _results);
  };
  
  const associateReceipt = (receipt: GameReceiptDto) => {
    new GameReceiptClient()
      .associateReceiptToGame(game.gameID, receipt.receiptID)
      .then((_response: GameReceiptDto) => {
        if(!_response) { alert('Failed to associate receipt!'); return; }
        onReceiptAdded(receipt);
      });
  };

    const addNewReceipt = () => {
        new GameReceiptClient().addReceipt(game.gameID)
            .then((_response: GameReceiptDto) => {
                if(!_response) {
                    alert('Failed to add game receipt');
                    return;
                }
                onReceiptAdded(_response);
            });
    };

    $: searchReceipts(searchTerm);
</script>

<div class="wrapper">
    <div class="controls d-flex">
        <button class="btn btn-success add-btn" on:click={addNewReceipt}>Add New</button>
        <input type="text" class="form-control" placeholder="Search for existing receipt" bind:value={searchTerm}>
    </div>
    <div class="search-results mt-3">
        {#if searchTerm === ''}
            <p>Enter in a search term to find a receipt.</p>
        {/if}
        {#each receipts as receipt}
            <div class="result d-flex mb-1">
                <span class="name">{receipt.receiptName}</span>
                <span class="number">{receipt.receiptNumber}</span>
                <span class="store">{receipt.store}</span>
                <button class="select btn btn-primary" on:click={() => associateReceipt(receipt)}>Select</button>
            </div>
        {/each}
    </div>
</div>