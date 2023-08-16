<script lang="ts">
  import { type BasicGameInfoDto, GameReceiptDto, GameReceiptClient } from "../../nlp-api";
	import GameReceiptAdder from "./GameReceiptAdder.svelte";
	import GameReceiptEditor from "./GameReceiptEditor.svelte";
  export let onReceiptAssociated: () => void;
  export let game: BasicGameInfoDto;
  let loading = true;
  let receipt: GameReceiptDto | undefined;

  const refreshReceiptInfo = (_game: BasicGameInfoDto | undefined) => {
    if(!_game) return;
    loading = true;
    receipt = undefined;

    new GameReceiptClient().getOrderInformation(_game.receiptID)
      .then((_receiptInfo: GameReceiptDto) => {
        if(!_receiptInfo) return;
        receipt = _receiptInfo;
      })
      .finally(() => loading = false);
  };

  const updateReceipt = (addedReceipt: GameReceiptDto) => {
    receipt = addedReceipt;
    onReceiptAssociated();
  };

  $: refreshReceiptInfo(game);
</script>

{#if !game}
  Please select a game first.
{:else}
    {#if loading}
      Loading...
    {:else}
      {#if receipt}
        <GameReceiptEditor {receipt} onReceiptChanged={updateReceipt} />
      {:else}
        <GameReceiptAdder {game} onReceiptAdded={updateReceipt} />
      {/if}
    {/if}
{/if}