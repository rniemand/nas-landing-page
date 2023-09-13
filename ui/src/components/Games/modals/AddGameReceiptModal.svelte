<script lang="ts">
  import { GameReceiptClient, type BasicGameInfoDto, GameReceiptDto } from "../../../nlp-api";
  export let game: BasicGameInfoDto;
  export let onReceiptAdded: (receipt: GameReceiptDto) => void;
  
  let searchTerm = '';
  let receipts: GameReceiptDto[] = [];
  
  const searchReceipts = async (term: string) => {
    if(term.length < 2) return;
    receipts = [];
    receipts = await new GameReceiptClient().search(term);
  };
  
  const associateReceipt = async (receipt: GameReceiptDto) => {
    const response = await new GameReceiptClient().associateReceiptToGame(game.gameID, receipt.receiptID);
    if(!response) {
      alert('Failed to associate receipt!');
      return;
    }
    onReceiptAdded(receipt);
  };

  const addNewReceipt = async () => {
    const response = await new GameReceiptClient().addReceipt(game.gameID);
    if(!response) {
        alert('Failed to add game receipt');
        return;
    }
    onReceiptAdded(response);
  };

  $: searchReceipts(searchTerm);
</script>

<div class="wrapper">
  <button class="btn btn-primary w-full my-2" on:click={addNewReceipt}>Add New</button>
  <input type="text" class="input input-bordered w-full mb-3" placeholder="Search for existing receipt" bind:value={searchTerm}>
  {#if searchTerm === ''}
    <p>Enter in a search term to find a receipt.</p>
  {/if}
  {#each receipts as receipt}
    <div class="flex mb-2 hover:bg-gray-900 rounded p-2 cursor-pointer">
      <span class="flex-1">{receipt.receiptName}</span>
      <span class="flex-1">{receipt.receiptNumber}</span>
      <span class="flex-1">{receipt.store}</span>
      <button class="btn btn-success btn-sm" on:click={() => associateReceipt(receipt)}>Select</button>
    </div>
  {/each}
</div>