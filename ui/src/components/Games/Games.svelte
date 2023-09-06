<script lang="ts">
	import { onMount } from "svelte";
  import { BasicGameInfoDto, GamePlatformsClient, GamePlatformDto } from "../../nlp-api";
	import GameAddModal from "./GameAddModal.svelte";
	import GameInfoModal from "./GameInfoModal.svelte";
  import GamePlatforms from "./GamePlatforms.svelte";
  import GameReceiptModal from "./GameReceiptModal.svelte";
	import GameSetConsoleModal from "./GameSetConsoleModal.svelte";
  import PlatformGameList from "./PlatformGameList.svelte";
	import { GameModal } from "./Games";

  let loading = true;
  let platforms: GamePlatformDto[];
  let selectedPlatform: GamePlatformDto | undefined;
  let selectedGame: BasicGameInfoDto;
  let gameList: PlatformGameList;
  // let genericModal: BSModal;
  let modalType: string = '';
  let modalTitle: string = '';
  let modalDialog: HTMLDialogElement;

  const MODAL_GAME_INFO = 'game-info';
  const MODAL_ADD_GAME = 'add-game';
  const MODAL_SET_CONSOLE = 'set-console';

  const platformSelected = (platform: GamePlatformDto) => selectedPlatform = platform;
  const onModalClosed = () => gameList.refresh();

  const triggerActionHandler = (action: string, game: BasicGameInfoDto | undefined) => {
    if(game) selectedGame = game;
    modalType = action;
    if(action === GameModal.Receipt) modalTitle = 'Game Receipt';
    if(action === MODAL_GAME_INFO) modalTitle = 'Game Info';
    if(action === MODAL_SET_CONSOLE) modalTitle = 'Set Console';
    if(action === MODAL_ADD_GAME) modalTitle = 'Add Game';
    modalDialog.showModal();
  };

  const closeModal = () => {
    modalDialog.close();
    onModalClosed();
  };
  
  (async () => {
    platforms = await new GamePlatformsClient().getAll();
    selectedPlatform = platforms ? platforms[0] : undefined;
    loading = false;
  })();
</script>

<div class="wrapper">
  {#if loading}
    Loading...
  {:else}
    <GamePlatforms {platforms} {selectedPlatform} onPlatformSelected={platformSelected} />
    <PlatformGameList {selectedPlatform} triggerAction={triggerActionHandler} bind:this={gameList} />
  {/if}
</div>

<div class="modal modal-lg fade" id="gameReceiptModal" tabindex="-1" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-4">{modalTitle}</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        {#if modalType === GameModal.Receipt}<GameReceiptModal game={selectedGame} onReceiptAssociated={closeModal} />{/if}
        {#if modalType === MODAL_GAME_INFO}<GameInfoModal game={selectedGame} onGameUpdated={closeModal} />{/if}
        {#if modalType === MODAL_ADD_GAME}<GameAddModal platform={selectedPlatform} onGameAdded={closeModal} />{/if}
        {#if modalType === MODAL_SET_CONSOLE}<GameSetConsoleModal game={selectedGame} onLocationSet={closeModal} />{/if}
      </div>
    </div>
  </div>
</div>

<dialog id="my_modal_2" class="modal" bind:this={modalDialog}>
  <div class="modal-box">
    <h3 class="font-bold text-lg">{modalTitle}</h3>
    {#if modalType === GameModal.Receipt}<GameReceiptModal game={selectedGame} onReceiptAssociated={closeModal} />{/if}
    {#if modalType === MODAL_GAME_INFO}<GameInfoModal game={selectedGame} onGameUpdated={closeModal} />{/if}
    {#if modalType === MODAL_ADD_GAME}<GameAddModal platform={selectedPlatform} onGameAdded={closeModal} />{/if}
    {#if modalType === MODAL_SET_CONSOLE}<GameSetConsoleModal game={selectedGame} onLocationSet={closeModal} />{/if}
  </div>
  <form method="dialog" class="modal-backdrop">
    <button>close</button>
  </form>
</dialog>

<!-- <button class="btn" onclick="my_modal_2.showModal()">open modal</button>
<dialog id="my_modal_2" class="modal">
  <div class="modal-box">
    <h3 class="font-bold text-lg">Hello!</h3>
    <p class="py-4">Press ESC key or click outside to close</p>
  </div>
  <form method="dialog" class="modal-backdrop">
    <button>close</button>
  </form>
</dialog> -->