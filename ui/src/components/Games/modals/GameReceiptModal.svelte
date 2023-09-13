<script lang="ts">
  import { type BasicGameInfoDto, GameReceiptDto, GameReceiptClient } from "../../../nlp-api";
	import AddGameReceiptModal from "./AddGameReceiptModal.svelte";
	import EditGameReceiptModal from "./EditGameReceiptModal.svelte";
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
        <EditGameReceiptModal {receipt} onReceiptChanged={updateReceipt} />
      {:else}
        <AddGameReceiptModal {game} onReceiptAdded={updateReceipt} />
      {/if}
    {/if}
{/if}