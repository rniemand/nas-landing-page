<style>
	.link { cursor: pointer; }
	.card { background-color: #ffffff; }
	.card:hover { background-color: #e0e0e0; }
	.link img {
		max-width: 100%;
		height: auto;
		border: 1px solid #e1e1e1;
		padding: 3px;
		border-radius: 4px;
		background-color: #fff;
		margin: 6px;
	}
	.d-flex { display: flex; }
	.d-flex span { flex: auto; text-align: center; }
	.d-flex .follow-count { color: #0d970d; }
	.d-flex .order { color: #8c8c8d; }
</style>

<script lang="ts">
	import { UserLinksClient, type UserLinkDto } from '../../nlp-api';
	export let link: UserLinkDto;

	const handleClick = () => {
		new UserLinksClient().recordLinkFollow(link.linkID).then((success: boolean) => {
			if(success) link.followCount += 1;
		});
		window.open(link.linkUrl, '_blank');
	};
</script>

<!-- svelte-ignore a11y-click-events-have-key-events -->
<div class="col link">
	<div class="card h-100" on:click={handleClick}>
		<div class="title">{link.linkName}</div>
		<img src={`api/images/link/${link.linkImage}`} alt={link.linkName} />
		<div class="card-body d-flex mb-0 p-1">
			<span class="id"># {link.linkID}</span>
			<span class="follow-count">{link.followCount}</span>
			<span class="order">{link.linkOrder}</span>
		</div>
	</div>
</div>