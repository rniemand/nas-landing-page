<script lang="ts">
	import { Accordion, AccordionItem, Col, Row } from 'sveltestrap';
	import AddChoreModal from './modals/AddChoreModal.svelte';
	import { ChoreClient, type HomeChoreDto } from '../../nlp-api';
	import ChoreInfoDisplay from './components/ChoreInfoDisplay.svelte';
	import EditChoreModal from './modals/EditChoreModal.svelte';

	let loading: boolean = true;
	let chores: HomeChoreDto[] = [];
	let editModal: EditChoreModal;

	const refreshChores = async () => {
		loading = true;
		chores = await new ChoreClient().getChores();
		loading = false;
	};

	const onChoreAdded = () => refreshChores();
	const onChoreUpdated = () => refreshChores();

	const onEditChore = (chore: HomeChoreDto) => {
		editModal?.editChore(chore);
	};

	refreshChores();
</script>

<Row>
	<Col class="text-end">
		<AddChoreModal {onChoreAdded} />
		<EditChoreModal bind:this={editModal} {onChoreUpdated} />
	</Col>
</Row>
<Row>
	<Col>
		<Accordion class="mt-1">
			{#each chores as chore}
				<AccordionItem header={chore.choreName}>
					<ChoreInfoDisplay {chore} {onEditChore} />
				</AccordionItem>
			{/each}
		</Accordion>
	</Col>
</Row>
