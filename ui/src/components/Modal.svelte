<style>
  .modal {
    display: none;
    position: fixed;
    z-index: 1;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    overflow: auto;
    background-color: rgb(0,0,0);
    background-color: rgb(56 85 161 / 44%);
  }
  .modal-content { background-color: #fefefe; margin: 15% auto; padding: 20px; border: 1px solid #888; width: 80%; color: #000; }
  .close { color: #aaa; font-size: 28px; font-weight: bold; }
  .controls { text-align: right; }
  .close:hover, .close:focus { color: black; text-decoration: none; cursor: pointer; }
  .open { display: block; }
</style>

<script lang="ts">
  let visible = false;

  export const open = () => {
    visible = true;
  };

  const closeModal = () => {
    visible = false;
  };
</script>

<div id="myModal" class="modal" class:open={visible}>
  <div class="modal-content">
    <div class="controls">
      <!-- svelte-ignore a11y-click-events-have-key-events -->
      <span class="close" on:click={closeModal}>&times;</span>
    </div>
    <slot />
  </div>
</div>