<style>
    .add {
        border: 2px solid #15ad15;
        padding: 6px;
        color: #044f07;
        background-color: #bdd3c7;
        border-radius: 6px;
        cursor: pointer;
        font-weight: bold;
        margin-right: 4px;
    }
    .controls {
        display: flex;
    }
    .controls input { flex: auto; }
    .search-results { margin-top: 12px; }
    .result { display: flex; margin-bottom: 2px; padding: 2px 7px; }
    .result:hover { background-color: #beff005c; border-radius: 4px; }
    .result span { flex: auto; padding-top: 7px; }
    span.name { width: 80px; text-align: left; }
    span.number { text-align: center; }
    span.store { text-align: right; }
    button.select {
        border: 1px solid #2020f9;
        margin-left: 4px;
        padding: 4px 9px;
        border-radius: 5px;
        background-color: #276da9;
        color: #fff;
        cursor: pointer;
    }
</style>

<script lang="ts">
	import { GameReceiptClient, type BasicGameInfoDto, ReceiptDto } from "../../nlp-api";

    export let game: BasicGameInfoDto;
    export let onReceiptAdded: (receipt: ReceiptDto) => void;
    let searchTerm = '';
    let receipts: ReceiptDto[] = [];

    const searchReceipts = (term: string) => {
        if(term.length < 2) return;
        receipts = [];
        new GameReceiptClient().search(term).then((_results: ReceiptDto[]) => receipts = _results);
    };

    const associateReceipt = (receipt: ReceiptDto) => {
        new GameReceiptClient().associateReceiptToGame(game.gameID, receipt.receiptID)
            .then((_response: ReceiptDto) => {
                if(!_response) {
                    alert('Failed to associate receipt!');
                    return;
                }
                onReceiptAdded(receipt);
            });
    };

    $: searchReceipts(searchTerm);
</script>

<div class="wrapper">
    <div class="controls">
        <button class="add">Add new Receipt</button>
        <input type="text" placeholder="Search for existing receipt" bind:value={searchTerm}>
    </div>
    <div class="search-results">
        {#if searchTerm === ''}
            Enter in a search term to find a receipt.
        {/if}
        {#each receipts as receipt}
            <div class="result">
                <span class="name">{receipt.receiptName}</span>
                <span class="number">{receipt.receiptNumber}</span>
                <span class="store">{receipt.store}</span>
                <button class="select" on:click={() => associateReceipt(receipt)}>Select</button>
            </div>
        {/each}
    </div>
</div>