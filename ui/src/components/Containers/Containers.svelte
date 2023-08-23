<script lang="ts">
  import { ContainerClient } from "../../nlp-api";
	import AddContainerModal from "./AddContainerModal.svelte";

  const refreshContainers = async () => {
    const response = await new ContainerClient().getAllContainers();
    console.log('response', response);
  };
  
  refreshContainers();
</script>

<div class="controls">
  <AddContainerModal onContainerAdded={refreshContainers} />
</div>
