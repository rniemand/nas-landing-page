<script lang="ts">
	import { Col, Row } from 'sveltestrap';
	import AddChoreModal from './modals/AddChoreModal.svelte';
	import { ChoreClient, type HomeChoreDto } from '../../nlp-api';

	let loading: boolean = true;
	let chores: HomeChoreDto[] = [];

	const refreshChores = async () => {
		loading = true;
		chores = await new ChoreClient().getChores();
		loading = false;
	};

	const onChoreAdded = () => {
		console.log('onChoreAdded');
		refreshChores();
	};

	refreshChores();
</script>

<Row>
	<Col class="text-end">
		<AddChoreModal {onChoreAdded} />
	</Col>
</Row>
<Row>
	<Col>
		{#each chores as chore}
			<div class="chore">
				{chore.choreName}
			</div>
		{/each}
	</Col>
</Row>
